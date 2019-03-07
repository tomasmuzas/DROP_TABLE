import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class AuthenticationPage extends React.Component {

    componentWillMount() {
        this.props.getAllAuthentication();
    }

    render() {
        if (this.props.authentication) {
            return (
                <div>
                    This is AuthenticationPage
                    {this.props.authentication.map(authentication => <div> {authentication} </div>)}
                </div>
            );
        }
        else {
            return (
                <div>
                    loading
                </div>
            )
        }
    }
}
const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        authentication: state.authentication
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(AuthenticationPage));
