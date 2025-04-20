import { BrowserRouter, Route, Routes  } from 'react-router-dom';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import Game from './pages/Game/Game';
import Home from './pages/Home/Home';
import MyList from './pages/MyList/MyList';
import SearchGames from './pages/SearchGames/SearchGames';
import { PageLayout } from './components/PageLayout/PageLayout';

export default function App() {

    return (
      <>
        <div className='app-background'>
            <PageLayout />
                <Routes>                
                  <Route index element={<Home/>}></Route>
                  <Route path='/search' Component={SearchGames}></Route>
                  <Route path='/my-list' Component={MyList}></Route>
                  <Route path='/game/:gameId' Component={Game}></Route>
                </Routes>
        </div>
       </>
    );
}
