import logo from "../assets/TELTONIKA-TELEMATICS.png";

export default function Navbar() {
  return (
    <nav className="navbar">
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
