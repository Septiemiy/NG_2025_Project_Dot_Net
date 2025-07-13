import axiosClient from "../../services/axiosClients";

const deviceTypes = ["light", "thermostat", "door_lock", "camera", "humidifier", "smart_plug"];
const rooms = ["kitchen", "livingroom", "bedroom", "bathroom"];

export default function RegisterDevice() {

  return (
    <div>
      <h2>Реєстрація девайсу</h2>
      <form>
        <div>
          <label>Назва девайсу</label>
          <input
            name="name" required
          />
        </div>
        <div>
          <label>Опис девайсу</label>
          <input
            name="description" required
          />
        </div>
        <div>
          <label>Тип девайсу</label>
          <select
            name="type" required
          >
            <option value="">-- Обрати тип --</option>
            {deviceTypes.map((t) => (
              <option key={t} value={t}>{t}</option>
            ))}
          </select>
        </div>
        <div>
          <label>Кімната</label>
          <select
            name="room" required
          >
            <option value="">-- Обрати кімнату --</option>
            {rooms.map((r) => (
              <option key={r} value={r}>{r}</option>
            ))}
          </select>
        </div>
        <button type="submit"></button>
      </form>
    </div>
  );
}
