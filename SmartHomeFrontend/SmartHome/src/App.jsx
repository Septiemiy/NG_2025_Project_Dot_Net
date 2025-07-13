import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import './App.css'
import Pages from './Pages'
import SmartHomePage from "./pages/SmartHomePage";
import DeviceDetailPage from "./pages/DeviceDetailPage";

function App() {

  return (
    <BrowserRouter>
      <Routes>
          <Route path="/" element={<SmartHomePage />} >
            <Route index element={<p>Choose device</p>} />
            <Route path="device/:deviceId" element={<DeviceDetailPage />} />
          </Route>
        {
          Pages.map((page, index) => {
            return <Route
              key={index}
              path={page.path}
              element={page.element}
            />
          })
        }
      </Routes>
    </BrowserRouter>
  )
}

export default App
