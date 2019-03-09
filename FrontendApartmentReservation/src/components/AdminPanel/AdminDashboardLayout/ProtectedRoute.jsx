import React from 'react';
import { Redirect, Route } from "react-router";

import { connect } from "react-redux";
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class ProtectedRoute extends React.Component {
    render() {
        if (sessionStorage.getItem('token') !== null || Object.keys(this.props.user).length !== 0) {
            return <Route {...this.props}/>
        }
        else {
            return <Redirect to="/login"/>
        }
    }
}

const mapDispatchToProps = (dispatch) => {
        return bindActionCreators(actionCreators, dispatch);
    }

    const mapStateToProps = (state) => {
        return {
            user: state.user
        };
    }

export default connect(mapStateToProps, mapDispatchToProps)(ProtectedRoute);