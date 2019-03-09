import React from 'react';
import { Switch } from 'react-router-dom';
import AdminDashboardNavigation from './AdminDashboardNavigation';
import { styles } from './styles';
import { withStyles } from '@material-ui/core';
import FlatsPage from '../FlatsPage/FlatsPage';
import DashboardPage from '../DashboardPage/DashboardPage';
import ProtectedRoute from './ProtectedRoute';

class AdminDashboardLayout extends React.Component {
    render() {
        const { classes } = this.props;
        return (
            <div className={classes.root}>
                <AdminDashboardNavigation />
                <div className={classes.content}>
                    <div className={classes.appBarSpacer}>
                        <Switch>
                            <ProtectedRoute path="/admin/flats" component={FlatsPage} />
                            <ProtectedRoute path="/admin" component={DashboardPage} />
                        </Switch>
                    </div>
                </div>
            </div>
        );
    }
}

export default withStyles(styles)(AdminDashboardLayout);
