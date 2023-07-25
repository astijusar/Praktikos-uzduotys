import { useRef, useState } from 'react'
import './css/FileUploadInput.css'

export default function FileUploadInput({ fileInputName, onFileSelect, label }) {
    const fileInputRef = useRef(null)
    const [selectedFileName, setSelectedFileName] = useState('')

    const handleFileSelect = () => {
      fileInputRef.current.click()
    };
  
    const handleFileChange = (event) => {
      const selectedFile = event.target.files[0]
      
      setSelectedFileName(selectedFile.name)
      onFileSelect(fileInputName, selectedFile)
    };

    return (
        <div className="container">
            <label htmlFor={`fileInputName-${fileInputName}`}>{label}</label>
            <div className="file-upload-box" onClick={handleFileSelect}>
                {selectedFileName 
                  ? (<div>File name: {selectedFileName}</div>) 
                  : (<div>Click to choose a file</div>)}
            </div>
            <input
                type="file"
                ref={fileInputRef}
                style={{ display: 'none' }}
                onChange={handleFileChange}
                id={`fileInput-${fileInputName}`}
            />
        </div>
    )
}