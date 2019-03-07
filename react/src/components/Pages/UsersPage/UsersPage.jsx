import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class UsersPage extends React.Component {

    componentWillMount() {
        this.props.getAllUsers();
    }

    render() {
        if (this.props.users) {
            return (
                <div>
                   <div>
                        This is UsersPage
                        {this.props.users.map(user => <div> {user} </div>)}
                    </div>
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
        users: state.users
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(UsersPage));
