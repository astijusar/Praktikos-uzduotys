import logo from "../assets/TELTONIKA-TELEMATICS.png";

export default function Navbar() {
  return (
    <nav className="navbar" style={{ backgroundColor: "#1e3d71" }}>
      <div className="container-md">
        <div className="row">
          <div className="col-4">
            <img
              className="img-fluid"
              src={logo}
              alt="Teltonika telematics logo"
            />
          </div>
        </div>
      </div>
    </nav>
  );
}
