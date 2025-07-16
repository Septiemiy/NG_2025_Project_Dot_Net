import { useEffect, useState, useRef } from "react";
import { useParams } from "react-router-dom";
import * as SignalR from "@microsoft/signalr";
import axiosClient from "../../services/axiosClients";
import { DEVICE_CONFIG } from "../../constants/deviceData";
import "./index.css";

export default function DevicePage() {
  const { deviceId } = useParams();
  const [device, setDevice] = useState(null);
  const [telemetry, setTelemetry] = useState({});
  const [error, setError] = useState("");
  const telemetryRef = useRef({});
  const token = localStorage.getItem("jwtToken");

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
              <div className="control-buttons">
                {config.commands.map((cmd) => (
                  <button key={cmd} onClick={() => alert(`Command: ${cmd}`)}>
                    {cmd}
                  </button>
                ))}
              </div>
            </section>
          )}

          {config.triggers.length > 0 && (
            <section className="control-section">
              <h3>Triggers</h3>
              <div className="control-buttons">
                {config.triggers.map((tr) => (
                  <button key={tr} onClick={() => alert(`Trigger: ${tr}`)}>
                    {tr}
                  </button>
                ))}
              </div>
            </section>
          )}

          {config.thresholds.length > 0 && (
            <section className="control-section">
              <h3>Thresholds</h3>
              <div className="control-buttons">
                {config.thresholds.map((th) => (
                  <div key={th}>
                    <label>{th}: </label>
                    <input
                      type="number"
                      placeholder={`Set ${th}`}
                      onBlur={(e) => alert(`Set ${th} to ${e.target.value}`)}
                    />
                  </div>
                ))}
              </div>
            </section>
          )}
        </div>
      </div>
    </>
  );
}
