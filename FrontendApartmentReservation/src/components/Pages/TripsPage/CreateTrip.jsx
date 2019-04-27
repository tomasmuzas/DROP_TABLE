import React from 'react';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import Select from 'react-select'
import * as actionCreators from '../../../actions';
import TextField from '@material-ui/core/TextField';


class CreateTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            departureDate: '',
            returnDate: '',
            employeesOptions: [],
            officesOptions: [],
            employeesPulled: false,
            selectedEmployees: [],
            selectedOffice: ''
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleDepartureDateChange = this.handleDepartureDateChange.bind(this);
        this.handleReturnDateChange = this.handleReturnDateChange.bind(this);
        this.handleEmployeesChange = this.handleEmployeesChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);
    }

    componentWillMount() {
        this.props.getAllOffices();
    }

    componentWillReceiveProps(newProps) {
        this.setEmployeesArray(newProps.employees);
        this.setOfficesArray(newProps.offices);
    }

    setEmployeesArray(employeesList) {
        if (employeesList && employeesList.length !== 0) {
            var employeesOptions = [];
            employeesList.forEach(function (employee) {
                var obj = { value: employee.id, label: employee.firstName + " " + employee.lastName }
                employeesOptions.push(obj);
            });

            this.setState({
                employeesOptions: employeesOptions,
                employeesPulled: true
            })
        }
    }

    setOfficesArray(officesList) {
        var officesOptions = [];
        officesList.forEach(function (office) {
            var obj = { value: office.id, label: office.address };
            officesOptions.push(obj);
        });
        this.setState({
            officesOptions: officesOptions
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { departureDate, returnDate, selectedEmployees, selectedOffice } = this.state;
        this.props.createTrip(selectedEmployees, selectedOffice, departureDate, returnDate);

    }

    handleEmployeesChange(e) {
        var employeesIdArray = [];
        e.forEach(function (employee) {
            employeesIdArray.push(employee.value);
        })
        this.setState({
            selectedEmployees: employeesIdArray
        })
    }

    handleOfficeChange(e) {
        this.setState({
            selectedOffice: e.value
        })
    }

    handleDepartureDateChange = date => {
        this.setState({ departureDate: date.target.value });
        if (this.state.returnDate !== '' && date.target.value !== '') {
            this.props.getAvailableEmployees(date.target.value, this.state.departureDate);
        }
    };

    handleReturnDateChange = date => {
        this.setState({ returnDate: date.target.value });
        if (this.state.departureDate !== '' && date.target.value !== '') {
            this.props.getAvailableEmployees(this.state.departureDate, date.target.value);
        }
    };

    render() {
        const { employeesPulled } = this.state;
        const { t } = this.props;
        return (
            <div className={`loginForm text-center jumbotron mx-auto col-12 pb-1 pt-4 row`}>
                <form className={`form-signin col-6`} onSubmit={this.handleSubmit}>
                    <div className="row">
                        <div className="col-6">
                            <TextField
                                id="departureDate"
                                label={t("DepartureDate")}
                                type="date"
                                onChange={this.handleDepartureDateChange}
                                InputLabelProps={{
                                    shrink: true,
                                }} />
                        </div>
                        <div className="form-group col-6">
                            <TextField
                                id="ReturnDate"
                                label={t("ReturnDate")}
                                type="date"
                                onChange={this.handleReturnDateChange}
                                InputLabelProps={{
                                    shrink: true,
                                }} />
                        </div>
                    </div>
                    <div hidden={!employeesPulled}>
                        <div className="form-group mb-2">
                            <Select
                                isMulti
                                options={this.state.employeesOptions}
                                className="basic-multi-select"
                                placeholder="Select employees"
                                onChange={this.handleEmployeesChange}
                                required />
                        </div>
                        <div className="form-group mb-2">
                            <Select
                                options={this.state.officesOptions}
                                className="basic-multi-select"
                                placeholder="Select office"
                                onChange={this.handleOfficeChange}
                                required
                            />
                        </div>
                        <Link className={`btn btn-lg btn-primary btn-block`} to={'/trip/' + this.props.trips.id} type="submit">{t("CreateTrip")}</Link>
                    </div>
                </form>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        employees: state.employees,
        offices: state.offices,
        trips: state.trips
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CreateTrip));