import { useEffect, useState, useRef } from "react";
import { useParams } from "react-router-dom";
import * as SignalR from "@microsoft/signalr";
import axiosClient from "../../services/axiosClients";
import { DEVICE_CONFIG } from "../../constants/deviceData";
import getUserRole from "../../services/getUserRole";
import "./index.css";

export default function DevicePage() {
  const { deviceId } = useParams();
  const [device, setDevice] = useState(null);
  const [telemetry, setTelemetry] = useState({});
  const [selectedCommand, setSelectedCommand] = useState("");
  const [requiresValue, setRequiresValue] = useState(false)
  const [commandValue, setCommandValue] = useState("")
  const [selectedThreshold, setSelectedThreshold] = useState("");
  const [thresholdValue, setThresholdValue] = useState("");
  const [successCommand, setSuccessCommand] = useState("");
  const [successThreshold, setSuccessThreshold] = useState("");
  const [error, setError] = useState("");
  const [errorCommand, setErrorCommand] = useState("");
  const [errorThreshold, setErrorThreshold] = useState("");
  const telemetryRef = useRef({});
  const token = localStorage.getItem("jwtToken");
  const userRole = getUserRole()
  const isAdmin = userRole === "Admin";

  useEffect(() => {
    (async () => {
      try {
        const response = await axiosClient.get(`/device/get/${deviceId}`);
        setDevice(response.data);
      } catch (error) {
        setError(`Could not load device data: ${error.message}`);
      }
    })();
  }, [deviceId]);

  useEffect(() => {
    if (!device) return;

    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:7026/api/hubs", {
        accessTokenFactory: () => token || ""
      })
      .configureLogging(SignalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    connection.start()
      .then(() => connection.invoke("Subscribe", deviceId))
      .catch(console.error);

    connection.on("ReceiveTelemetryData", (data) => {
      console.log(data)
      const { timestamp, dataType, dataValue } = data;
      
      telemetryRef.current[dataType] = {
        value: dataValue ?? telemetryRef.current[dataType]?.value ?? "–",
        timestamp:
          timestamp && !isNaN(Date.parse(timestamp))
            ? new Date(timestamp).toLocaleTimeString()
            : telemetryRef.current[dataType]?.timestamp ?? "–"
      };

      setTelemetry({ ...telemetryRef.current });
    });

    connection.onclose(error => {
      console.error("Connection closed due to error: ", error);
    });

    return () => {
      connection.invoke("Unsubscribe", deviceId)
        .catch(console.error)
        .finally(() => connection.stop());
    };
  }, [device, deviceId, token]);

  const handleSendCommand = async (command, value) => {
    setSuccessCommand("");
    if(requiresValue && !value.trim()) {
      return
    }

    try {
      const response = await axiosClient.post('/command/sendCommand', {
        deviceId: deviceId,
        commandName: command,
        value: requiresValue ? value : null,
        role: userRole
      })
      setSuccessCommand("Command sent");
      setSelectedCommand("");
      setCommandValue("");
      setRequiresValue(false);
      setErrorCommand("");
    } catch(error) {
      setErrorCommand(error.message);
      console.log("Failed to send command: ", error)
    }
  };

  const handleSetThreshold = async (threshold, value) => {
    setSuccessThreshold("");
    if (!value.trim()) {
      return;
    }

    try {
      const response = await axiosClient.post('/threshold/createThreshold', {
        deviceId: deviceId,
        thresholdCondition: threshold,
        value: value
      })

      setSuccessThreshold("Threshold sent to create");
      setSelectedThreshold("");
      setThresholdValue("");
      setErrorThreshold("");
    } catch(error) {
      setErrorThreshold(error.message);
      console.log("Failed to create threshold: ", error)
    }
  };

  if (error) return <p>{error}</p>;
  if (!device) return <p>Loading device...</p>;

  const config = DEVICE_CONFIG.find((cfg) => cfg.type === device.type) || {
    commands: [],
    triggers: [],
    thresholds: []
  };

  return (
    <>
      <div className="device-info-panel">
        <div className="left-column">
          <div><strong>Name:</strong> {device.name}</div>
          <div><strong>Type:</strong> {device.type}</div>
          <div><strong>Added by:</strong> {device.userId}</div>
        </div>
        <div className="right-column">
          <div><strong>Location:</strong> {device.location}</div>
          <div><strong>Discription:</strong> {device.description}</div>
          <div><strong>Created at:</strong> {new Date(device.createdAt).toLocaleString()}</div>
        </div>
      </div>
      <div className="device-page">
        <div className="device-left">
          <section>
            <h3>Telemetry</h3>
            <ul className="telemetry-table">
              {config.telemetryTypes.map((type) => (
                <li key={type} className="telemetry-row">
                  <span className="telemetry-label">{type}</span>
                  <span className="telemetry-value">
                    {telemetry[type]?.value ?? "–"}
                    {config.telemetryMeasurements[config.telemetryTypes.indexOf(type)]}
                  </span>
                  <span className="telemetry-timestamp">
                    {telemetry[type]?.timestamp ?? "–"}
                  </span>
                </li>
              ))}
            </ul>
        </section>
        </div>
        <div className="device-right">
            {config.commands.length > 0 && (
              <section className="control-section">
                <h3>Commands</h3>
                {successCommand && <div className="success-message">{successCommand}</div>}
                {errorCommand && <div className="error-message">{errorCommand}</div>}
                <div className="control-vertical">
                  <div className="control-inline">
                    <select value={selectedCommand} onChange={(e) => {
                      const cmdName = e.target.value
                      setSelectedCommand(cmdName)
                      const cmdObject = config.commands.find(c => c.name === cmdName)
                      setRequiresValue(cmdObject?.requiresValue || false)
                    }}>
                      <option value="">Select a command</option>
                      {config.commands.map((cmd) => (
                        <option key={cmd.name} value={cmd.name}>{cmd.name}</option>
                      ))}
                    </select>
                    <button disabled={!selectedCommand || (requiresValue && !commandValue)} onClick={() => handleSendCommand(selectedCommand, commandValue)}>
                      Send Command
                    </button>
                  </div>
                  {requiresValue && (
                    <input type="text" placeholder="Enter value" value={commandValue} onChange={(e) => setCommandValue(e.target.value)} />
                  )}
                </div>
              </section>
            )}

            {config.triggers.length > 0 && (
              <section className="control-section">
                <h3>Provided Triggers</h3>
                <div className="control-triggers">
                  {config.triggers.map((trigger, index) => (
                    <input type="text" key={index} value={trigger} disabled/>
                  ))}
                </div>
              </section>
            )}

            {isAdmin && config.thresholds.length > 0 && (
              <section className="control-section">
                <h3>Thresholds</h3>
                {successThreshold && <div className="success-message">{successThreshold}</div>}
                {errorThreshold && <div className="error-message">{errorThreshold}</div>}
                <div className="control-inline">
                  <select value={selectedThreshold} onChange={(e) => setSelectedThreshold(e.target.value)}>
                    <option value="">Select a threshold</option>
                    {config.thresholds.map((th) => (
                      <option key={th} value={th}>{th}</option>
                    ))}
                  </select>
                  <button disabled={!selectedThreshold || !thresholdValue} onClick={() => handleSetThreshold(selectedThreshold, thresholdValue)}>
                    Set Threshold
                  </button>
                </div>
                <input type="text" placeholder="Enter value" value={thresholdValue} onChange={(e) => setThresholdValue(e.target.value)}/>
              </section>
            )}
          </div>
        </div>
    </>
  );
}
