import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class UsersPage extends React.Component {

    componentWillMount() {
        this.props.getAllEmployees();
    }

    render() {
        if (this.props.employees) {
            return (
                <div>
                   <div>
                        This is UsersPage
                        {this.props.employees.map(employee => <div> {employee.firstName} </div>)}
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
        employees: state.employees
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(UsersPage));
