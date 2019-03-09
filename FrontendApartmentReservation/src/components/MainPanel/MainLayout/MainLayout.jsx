import React from 'react';
import { Route, Switch } from 'react-router-dom';
import MainPage from '../../Pages/MainPage/MainPage';
import PageNotFound from '../../Pages/PageNotFound/PageNotFound';
import ApartamentsPage from '../../Pages/ApartmentsPage/ApartmentsPage';
import AuthenticationPage from '../../Pages/AuthenticationPage/AuthenticationPage';
import UsersPage from '../../Pages/UsersPage/UsersPage';
import TripsPage from '../../Pages/TripsPage/TripsPage';
import PageHeader from './PageHeader';

export default class MainLayout extends React.Component {
    render() {
        return (
            <div>
                <div>
                    <PageHeader/>
                </div>
                <Switch className="content">
                    <Route exact path="/" component={MainPage} />
                    <Route path="/apartments" component={ApartamentsPage}/>
                    <Route path="/authentication" component={AuthenticationPage}/>
                    <Route path="/trips" component={TripsPage}/>
                    <Route path="/users" component={UsersPage}/>
                    <Route path="*" component={PageNotFound}/>
                </Switch>
            </div>
        );
    }
}