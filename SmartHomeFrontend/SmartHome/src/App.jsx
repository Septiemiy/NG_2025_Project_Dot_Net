import { BrowserRouter, Route, Routes } from "react-router-dom";
import './App.css'
import Pages from './Pages'

function App() {

  return (
    <BrowserRouter>
      <Routes>
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
