export const DEVICE_CONFIG = [
  {
    type: "light",
    telemetryTypes: ["Brightness", "Power state"],
    telemetryMeasurements: [" %"],
    commands: [
        { name: "Turn On", "requiresValue": false},
        { name: "Turn Off", "requiresValue": false},
        { name: "Set Brightness", "requiresValue": true}
      ],
    triggers: ["Auto-on at 7:00", "Auto-off at 23:00"],
    thresholds: ["Brightness <"]
  },
  {
    type: "temperatureSensor",
    telemetryTypes: ["Temperature"],
    telemetryMeasurements: [" °C"],
    commands: [],
    triggers: [],
    thresholds: ["Temperature >", "Temperature <"]
  },
  {
    type: "thermostat",
    telemetryTypes: ["Current Temperature"],
    telemetryMeasurements: [" °C"],
    commands: [
      { name: "Set Temperature", requiresValue: true },
      { name: "Turn On", requiresValue: false },
      { name: "Turn Off", requiresValue: false }
    ],
    triggers: [],
    thresholds: ["Turn On if temperature < ", "Turn Off if temperature > "]
  },
  {
    type: "humidity",
    telemetryTypes: ["Humidity"],
    telemetryMeasurements: [" %"],
    commands: [],
    triggers: [],
    thresholds: ["Humidity >", "Humidity <"]
  },
  {
    type: "door",
    telemetryTypes: ["Door state"],
    telemetryMeasurements: [],
    commands: [
      { name: "Open", requiresValue: false },
      { name: "Close", requiresValue: false },
      { name: "Lock", requiresValue: false },
      { name: "Unlock", requiresValue: false }
    ],
    triggers: ["Auto Lock after 22:00"],
    thresholds: ["Door Open Time >  minutes"]
  },
  {
    type: "conditioner",
    telemetryTypes: ["Temperature", "Power state"],
    telemetryMeasurements: [" °C" ],
    commands: [
      { name: "Set Temperature", requiresValue: true },
      { name: "Turn On", requiresValue: false },
      { name: "Turn Off", requiresValue: false }
    ],
    triggers: [],
    thresholds: ["Temperature >", "Temperature <"]
  },
  {
    type: "bathroomShower",
    telemetryTypes: ["Water Temperature"],
    telemetryMeasurements: [" °C"],
    commands: [
      { name: "Set Temperature", requiresValue: true },
      { name: "Turn On", requiresValue: false },
      { name: "Turn Off", requiresValue: false }
    ],
    triggers: [],
    thresholds: ["Temperature >", "Temperature <"]
  },
  {
    type: "washingMachine",
    telemetryTypes: ["Cycle Status", "Remaining Time"],
    telemetryMeasurements: ["", " minutes"],
    commands: [
      { name: "Start Cycle", requiresValue: false },
      { name: "Stop Cycle", requiresValue: false },
      { name: "Set Cycle Type", requiresValue: true }
    ],
    triggers: [],
    thresholds: []
  },
  {
    type: "smokeAlarm",
    telemetryTypes: ["Smoke Detected", "Battery Status"],
    telemetryMeasurements: ["", " %"],
    commands: [
      { name: "Test Alarm", requiresValue: false },
      { name: "Silence Alarm", requiresValue: false }
    ],
    triggers: ["Auto test every week"],
    thresholds: ["Smoke Detected", "Battery Status < "]
  }
];