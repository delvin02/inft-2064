import './App.css';
import Card from './components/Card';
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV2';
import CardList from './components/CardList';

function App() {
  return (
      <div className="App container">
          <div className="bg-light py-1 mb-2">
              <h2 className="text-center">Example Application</h2>
          </div>
          <div className="row justify-center gap-2">
              {/*<Card itemId="1" itemName="Test record1" itemDescription="Test record 1 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
              {/*<CardV2 itemId="2" itemName="Test record2" itemDescription="Test record 2 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
              {/*<CardV3 itemId="3" itemName="Test record3" itemDescription="Test record 3 desc" itemCost="15.00" itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" />*/}
              <CardList/>
          </div>

    </div>
  );
}

export default App;
