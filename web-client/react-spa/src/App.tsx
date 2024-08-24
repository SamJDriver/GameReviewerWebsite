import { BrowserRouter, Route, Routes  } from 'react-router-dom';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import Game from './pages/Game/Game';
import Home from './pages/Home';

export default function App() {

    return (
        <div style={{ backgroundColor: "#2A3440", height: "100vh" }}>
              <BrowserRouter>
                <Routes>                
                  <Route index element={<Home/>}></Route>
                  <Route path='/game/:gameId' Component={Game}></Route>
                </Routes>
              </BrowserRouter>
       </div>
    );
}
