import { BrowserRouter, Route, Routes  } from 'react-router-dom';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import Game from './pages/Game/Game';
import Home from './pages/Home/Home';
import { GameSearch } from './components/GameSearch/GameSearch';

export default function App() {

    return (
        <div className='app-background'>
              <BrowserRouter>
                <Routes>                
                  <Route index element={<Home/>}></Route>
                  <Route path='/search' Component={GameSearch}></Route>
                  <Route path='/game/:gameId' Component={Game}></Route>
                </Routes>
              </BrowserRouter>
       </div>
    );
}
