import { Component } from 'react';
import DataLoader from 'components/dataLoader/dataLoader';
import Enter from 'pages/enter/enter';
import Grids from 'pages/grids/grids';
import Olympic from 'pages/grids/olympic/olympic';
import {
  BrowserRouter,
  Route,
  Switch
} from 'react-router-dom';

class Router extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route exact path='/' component={DataLoader}/>
          <Route path='/enter' component={Enter}/>
          <Route path='/grids' component={Grids}/>
          <Route path='/grid/olympic' component={Olympic}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default Router;
