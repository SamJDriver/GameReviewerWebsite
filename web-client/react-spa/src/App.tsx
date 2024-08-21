import { BrowserRouter, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import Game from './pages/Game';
import Home from './pages/Home';

export default function App() {

    return (
        <div style={{ backgroundColor: "#2A3440", height: "100vh" }}>
              <BrowserRouter>
                <Routes>                
                  <Route index element={<Home/>}></Route>
                  <Route path='/Game' element={<Game/>}></Route>
                </Routes>
              </BrowserRouter>
       </div>
    );
}
