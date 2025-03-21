import { useEffect, useState } from 'react';
import './App.css';

interface Pet {
  id: string;
  name: string;
  birthdate: string;
  stats: null;
}

function App() {
  const [pet, setPet] = useState<Pet>();

  useEffect(() => {
    populateWeatherData();
  }, []);

  return (
    <div>
      <h1 id='tableLabel'>{pet?.name}</h1>
    </div>
  );

  async function populateWeatherData() {
    const response = await fetch(
      'api/pet?id=68FCC4F4-1560-41C4-B363-89B95AC5BC12'
    );
    if (response.ok) {
      const data = await response.json();
      console.log(data);
      setPet(data.content);
    }
  }
}

export default App;
