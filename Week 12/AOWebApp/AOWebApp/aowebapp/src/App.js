import './App.css';
import Card from './components/Card';
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV2';
import CardList from './components/CardList';
import { Link, Outlet } from "react-router-dom";

function App() {
  return (
      <div className="App container">
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
              <div className="container-fluid">
                  <Link className="navbar-brand" href="#">Navbar</Link>
                  <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                      <span className="navbar-toggler-icon"></span>
                  </button>
                  <div className="collapse navbar-collapse" id="navbarNav">
                      <div className="navbar-nav">
                        <Link className="nav-link active" aria-current="page" to="/Home">Home</Link>
                        <Link className="nav-link" to="/Contact">Contact</Link>
                        <Link className="nav-link" to="/Products">Products</Link>
                    </div>
                  </div>
              </div>
          </nav>
          <Outlet />
          {/*<div className="row justify-center gap-2">*/}
          {/*    */}{/*<Card itemId="1" itemName="Test record1" itemDescription="Test record 1 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
          {/*    */}{/*<CardV2 itemId="2" itemName="Test record2" itemDescription="Test record 2 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
          {/*    */}{/*<CardV3 itemId="3" itemName="Test record3" itemDescription="Test record 3 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
          {/*    <CardList/>*/}
          {/*</div>*/}

    </div>
  );
}

export default App;
