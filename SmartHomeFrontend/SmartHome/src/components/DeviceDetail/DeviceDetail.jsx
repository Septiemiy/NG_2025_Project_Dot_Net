import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import * as SignalR from "@microsoft/signalr";

export default function DevicePage() {
  const { deviceId } = useParams();
  const [telemetry, setTelemetry] = useState('');
  const token = localStorage.getItem("jwtToken");

  useEffect(() => {
    console.log("Connecting to SignalR hub...");
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:7026/api/hubs", {
        withCredentials: true   
      })
      .configureLogging(SignalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    connection.start()
      .then(() => {
        console.log("Connection started");
        return connection.invoke("Subscribe", deviceId);
      })
      .catch(error => {
        console.error("SignalR Connection error: ", error);
      });

    connection.on("ReceiveTelemetryData", data => {
      console.log("Received telemetry data:", data);
      setTelemetry(data);
    });

    return () => {
      connection.invoke("Unsubscribe", deviceId)
        .catch(console.error)
        .finally(() => connection.stop());
    };
  }, [deviceId]);

  return (
    <div>
      <h2>Device {deviceId}</h2>
      {telemetry
        ? <pre>{JSON.stringify(telemetry, null, 2)}</pre>
        : <p>Waiting for telemetry…</p>}
    </div>
  );
}
