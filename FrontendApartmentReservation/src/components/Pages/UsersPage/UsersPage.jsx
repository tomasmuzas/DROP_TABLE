import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import { GridLoader } from "react-spinners";

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
                <div className="center-outer-div">
                    <div className='center-div'>
                        <GridLoader
                            sizeUnit={"px"}
                            size={50}
                            color={'red'}
                        />
                    </div>
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
