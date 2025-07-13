import { useState } from "react";
import axiosClient from "../api/axiosClient";

const deviceTypes = ["light", "thermostat", "door_lock", "camera", "humidifier", "smart_plug"];
const rooms = ["kitchen", "livingroom", "bedroom", "bathroom"];

export default function RegisterDevice() {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);
    setSuccess(false);

    try {
      await axiosClient.post("/api/devices/register", {
        ...form,
        deviceId: crypto.randomUUID(), // ⬅️ генеруємо GUID на клієнті
      });
      setSuccess(true);
      setForm({ name: "", type: "", room: "" });
    } catch (err: any) {
      setError("Помилка при реєстрації девайсу");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-8 p-4 border rounded shadow">
      <h2 className="text-2xl font-semibold mb-4">Реєстрація девайсу</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block mb-1 font-medium">Назва девайсу</label>
          <input
            name="name"
            value={form.name}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2"
            required
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">Тип девайсу</label>
          <select
            name="type"
            value={form.type}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2"
            required
          >
            <option value="">-- Обрати тип --</option>
            {deviceTypes.map((t) => (
              <option key={t} value={t}>{t}</option>
            ))}
          </select>
        </div>

        <div>
          <label className="block mb-1 font-medium">Кімната</label>
          <select
            name="room"
            value={form.room}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2"
            required
          >
            <option value="">-- Обрати кімнату --</option>
            {rooms.map((r) => (
              <option key={r} value={r}>{r}</option>
            ))}
          </select>
        </div>

        <button
          type="submit"
          disabled={isSubmitting}
          className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded"
        >
          {isSubmitting ? "Реєстрація..." : "Зареєструвати"}
        </button>

        {success && <p className="text-green-600 mt-2">Девайс зареєстровано успішно!</p>}
        {error && <p className="text-red-600 mt-2">{error}</p>}
      </form>
    </div>
  );
}
