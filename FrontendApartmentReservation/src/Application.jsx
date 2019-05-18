import { createStore, combineReducers, applyMiddleware, compose } from 'redux';
import React from 'react';
import { Provider } from 'react-redux';
import { reducer as formReducer } from 'redux-form';
import createHistory from 'history/createBrowserHistory';
import { ConnectedRouter, routerReducer, routerMiddleware } from 'react-router-redux';
import { Route, Switch } from 'react-router-dom';
import reducers from './reducers/index';
import thunk from 'redux-thunk';
import MainLayout from './components/MainPanel/MainLayout/MainLayout';
import NotFoundPage from './components/Pages/PageNotFound/PageNotFound';
import LoginPage from './components/Pages/LoginPage/LoginPage';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';

const historyObj = createHistory();
historyObj.listen(_ => {
    window.scrollTo(0, 0)  
})
const middleware = [
    routerMiddleware(historyObj)
];

const composeEnhancer = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const createdStore = createStore(
    combineReducers({
        ...reducers,
        router: routerReducer,
        form: formReducer
    }),
    composeEnhancer(applyMiddleware(...middleware, thunk))
);

const Application = () => (
    <MuiThemeProvider theme={theme}>
        <Provider store={createdStore}>
            <ConnectedRouter history={historyObj} >
            
                <Switch>
                    <Route exact path="" component={MainLayout}/>
                    <Route path ="*" component={NotFoundPage}/>
                </Switch>
            </ConnectedRouter>
        </Provider>
    </MuiThemeProvider>
);

const theme = createMuiTheme({
    typography: {
        useNextVariants: true,
        "fontFamily": "\"Calibri\""
    }
 });

export default Application;