import React from 'react';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import Select from 'react-select'
import * as actionCreators from '../../../actions';
import TextField from '@material-ui/core/TextField';


class CreateTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputCost: '',
            departureDate: '',
            returnDate: '',
            employeesOptions: [],
            officesOptions: [],
            employeesPulled: false,
            selectedEmployees: [],
            selectedOffice: ''
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleCostChange = this.handleCostChange.bind(this);
        this.handleDepartureDateChange = this.handleDepartureDateChange.bind(this);
        this.handleReturnDateChange = this.handleReturnDateChange.bind(this);
        this.handleEmployeesChange = this.handleEmployeesChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);
    }

    componentWillMount() {
        //this.props.getAllOffices();
        var officeOptions = [];
        var employers = [
            { Id: 1, FirstName: "Martynas", LastName: "Narijauskas" },
            { Id: 2, FirstName: "Tomas", LastName: "Mūžas" },
            { Id: 3, FirstName: "Andrėjus", LastName: "Kuznecovas" }
        ];

        this.setEmployeesArray(employers);
        var dbOffices = [
            { Id: 1, Address: "Vilnius didlaukio 59" },
            { Id: 2, Address: "Kaunas žalgirio 25" },
            { Id: 3, Address: "Londonas main square" }
        ]

        dbOffices.forEach(function (office) {
            var obj = { value: office.Id, label: office.Address };
            officeOptions.push(obj);
        });
        this.setState({
            officesOptions: officeOptions
        })
    }

    setEmployeesArray(employeesList) {
        var employeesOptions = [];
        employeesList.forEach(function (employee) {
            var obj = { value: employee.Id, label: employee.FirstName + " " + employee.LastName }
            employeesOptions.push(obj);
        });

        this.setState({
            employeesOptions: employeesOptions
        })
    }

    handleSubmit(e) {
        e.preventDefault();
        const { inputCost, departureDate, returnDate, selectedEmployees, selectedOffice } = this.state;
        this.props.createTrip(inputCost, departureDate, returnDate, selectedEmployees, selectedOffice);
        this.props.createGroup(selectedEmployees);
    }

    handleCostChange(e) {
        this.setState({
            inputCost: e.target.value
        });
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
            this.props.getAvailableEmployees(date.target.value, this.state.departureDate)
            this.setEmployeesArray(this.props.employees)
            this.setState({
                employeesPulled: true
            })
        }
        else {
            this.setState({
                employeesPulled: false
            })
        }
    };

    handleReturnDateChange = date => {
        this.setState({ returnDate: date.target.value });
        if (this.state.departureDate !== '' && date.target.value !== '') {
            this.props.getAvailableEmployees(this.state.departureDate, date.target.value);
            this.setState({
                employeesPulled: true,
            })
        }
        else {
            this.setState({
                employeesPulled: false
            })
        }
    };

    render() {
        const { inputCost, employeesPulled } = this.state;
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
                            <input type="text" id="inputCost" className={`form-control`} placeholder={t("Cost")}
                                required autoFocus name="inputCost" value={inputCost}
                                onChange={this.handleCostChange} />
                        </div>
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
                        <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("CreateTrip")}</button>
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
        employees: state.employees
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CreateTrip));