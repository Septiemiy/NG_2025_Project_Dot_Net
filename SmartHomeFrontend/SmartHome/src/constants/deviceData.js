export const DEVICE_CONFIG = [
  {
    type: "light",
    telemetryTypes: ["Brightness", "Power state"],
    commands: ["Turn On", "Turn Off", "Set Brightness"],
    triggers: ["Auto-off at night"],
    thresholds: ["Brightness"]
  },
  {
    type: "thermostat",
    telemetryTypes: ["temperature", "humidity"],
    commands: ["Set Target Temperature"],
    triggers: ["Turn heat on < 18°C"],
    thresholds: ["Temp"]
  },
  {
    type: "camera",
    telemetryTypes: ["motionDetected"],
    commands: ["Start Recording", "Stop Recording"],
    triggers: ["Alert on motion"],
    thresholds: []
  }
];