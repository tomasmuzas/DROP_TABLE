import React from 'react';
import { Route, Switch } from 'react-router-dom';
import PageNotFound from '../../Pages/PageNotFound/PageNotFound';
import ApartamentsPage from '../../Pages/ApartmentsPage/ApartmentsPage';
import UsersPage from '../../Pages/UsersPage/UsersPage';
import TripsPage from '../../Pages/TripsPage/TripsPage';
import PageHeader from './PageHeader';
import SignUpPage from '../../Pages/SignUpPage/SignUpPage';
import CreateTrip from '../../Pages/TripsPage/CreateTrip';
import TripCheckList from '../../Pages/CheckListPage/TripCheckList';
import CheckListDetails from '../../Pages/CheckListPage/CheckListFullDetails/CheckListDetails';
import LoginPage from '../../Pages/SignUpPage/LoginPage';
import MergeTrips from '../../Pages/TripsPage/MergeTrips';
import MyTrips from '../../Pages/MyInfo/MyTripsPage';

export default class MainLayout extends React.Component {
    render() {
        return (
            <div>
                <div>
                    <PageHeader/>
                </div>
                <Switch className="content">
                    <Route exact path="/" component={LoginPage} />
                    <Route path="/apartments" component={ApartamentsPage}/>
                    <Route path="/trips" component={TripsPage}/>
                    <Route path="/users" component={UsersPage}/>
                    <Route path="/signup/:employeeId" component={SignUpPage}/>
                    <Route path="/signup" component={SignUpPage}/>
                    <Route path="/login" component={LoginPage}/>
                    <Route path="/createTrip" component={CreateTrip}/>
                    <Route path="/trip/:tripId/merge" component={MergeTrips}/>
                    <Route path="/trip/:tripId" component={TripCheckList}/>
                    <Route path="/:tripId/checklist/:employeeId" component={CheckListDetails}/>
                    <Route path="/myInfo/myTrips" component={MyTrips}/>
                    <Route path="*" component={PageNotFound}/>
                </Switch>
            </div>
        );
    }
}