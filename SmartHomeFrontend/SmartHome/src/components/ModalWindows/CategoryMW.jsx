import "./index.css"
import { useEffect, useState } from "react"

export default function CategoryMW({isOpen, onClose, onSave, categoryName, title}) {
    const [name, setName] = useState("")
    const [error, setError] = useState("")

    useEffect(() => {
        setName(categoryName)
    }, [categoryName])

    if(!isOpen) return null

    const handleSave = () => {
        if(!name.trim()) {
            setError("Category name is empty")
            return
        }

        setError("")
        onSave(name)
        setName("")
    }

    const handleClose = () => {
        setError("")
        setName("")

        onClose()
    }

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <h3>{title}</h3>
                {error && <div className="input-error">{error}</div>}
                <input type="text" placeholder="Enter category name" value={name}
                    onChange={(e) => setName(e.target.value)} />
                <div className="modal-buttons">
                    <button onClick={handleSave}>Save</button>
                    <button onClick={handleClose}>Cancel</button>
                </div>
            </div>
        </div>
    );
}