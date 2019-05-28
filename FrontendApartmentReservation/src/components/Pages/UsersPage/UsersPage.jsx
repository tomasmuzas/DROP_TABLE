import React from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import { GridLoader } from "react-spinners";
import UserCard from './UserCard';
import { withTranslation } from 'react-i18next';


class UsersPage extends React.Component {

    componentWillMount() {
        this.props.getEmployeesWithRoles();
    }

    render() {
        const { t } = this.props;
        if (this.props.employees === []) {
            return (<div className="row mt-5">
                <div className="col-12">
                    <h1>{t('EmptyEmployeesList')}</h1>
                </div>
            </div>
            )
        }
        else if (this.props.employees.length > 0) {
            return (
                <div className="row mt-5">
                    <div className="col-12">
                        <h1 style={{ textAlign: 'center' }}>{t("Employees")}</h1>
                        <div>
                            {this.props.employees.map(employee => <UserCard employee={employee} key={employee.id} />)}
                        </div>
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

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(UsersPage));
