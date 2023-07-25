import "./css/FileInformationCard.css";

export default function FileInformationCard({ file, label }) {
    const fileInformation = file.metadata.map((item) => {
        const [key, value] = item.split(":");
        return { id: key.trim(), value: value.trim()};
    });

    return (
    <div className="container">
      <div className="card">
        <div
          className="card-header"
          style={{ backgroundColor: "#1e3d71", color: "white" }}
        >
          {label} <span className="fw-bolder">{file.fileName}</span>
        </div>
        <div className="card-body">
          {fileInformation.map((item) => 
            <p key={item.id}><span className="fw-bolder">{item.id}: </span>{item.value}</p>
          )}
        </div>
      </div>
    </div>
  );
}
