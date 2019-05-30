import React from 'react';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import Select from 'react-select';
import i18next from 'i18next';

class SignUpPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputName: '',
            inputSurname: '',
            inputEmail: '',
            inputPassword: '',
            selectedOffice: '',
            officesOptions: [],
            rolesOptions: [{ value: 2, label: i18next.t('Admin') }, { value: 1, label: i18next.t('Organizer') }, { value: 0, label: i18next.t('Regular') }],
            selectedRole: '',
            isEditing: false,
            employee: '',
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleSurnameChange = this.handleSurnameChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);
        this.handleRoleChange = this.handleRoleChange.bind(this);
        this.updateUser = this.updateUser.bind(this);

    }

    componentWillMount() {
        this.props.getAllOffices();
        var isEditingParmsNotNull = this.props.match.params.employeeId ? true : false;
        this.setState({
            isEditing: isEditingParmsNotNull,
        })
        if (isEditingParmsNotNull) {
            if (this.props.employees && this.props.employee !== [] && this.props.employees.length !== 0) {
                var employee = this.props.employees.find(employee => employee.id === this.props.match.params.employeeId);
                this.setStateWithEmployee(employee);
            }
            else {
                this.props.getEmployeeById(this.props.match.params.employeeId)
            }
        }
    }

    setStateWithEmployee(employee) {
        this.setState({
            inputName: employee.firstName,
            inputSurname: employee.lastName,
            inputEmail: employee.email,
            selectedRole: employee.role,
            selectedOffice : employee.office
        })
    }

    componentWillReceiveProps(newProps) {
        this.setOfficesArray(newProps.offices);
        if(this.state.isEditing && this.props.employees !== null && this.props.employees.length === 0 && newProps.employees !== [] && newProps.employees.length !== 0){
            this.setStateWithEmployee(newProps.employees)
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
        const { inputName, inputSurname, inputEmail, inputPassword, selectedOffice, selectedRole } = this.state;
        const regex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*d).{8,}$', 'i');
        if (regex.test(inputPassword)) {
            this.props.signUpUser(inputName, inputSurname, inputEmail, inputPassword, selectedOffice, selectedRole);
        } else {
            alert(this.props.t("PasswordError"));
        }
    }

    updateUser() {
        const { inputName, inputSurname, inputEmail, selectedOffice, selectedRole } = this.state;
        this.props.updateUser(inputName, inputSurname, inputEmail, selectedOffice, selectedRole, this.props.match.params.employeeId);
    }

    handleNameChange(e) {
        this.setState({
            inputName: e.target.value
        });
    }

    handleSurnameChange(e) {
        this.setState({
            inputSurname: e.target.value
        });
    }

    handleEmailChange(e) {
        this.setState({
            inputEmail: e.target.value
        });
    }

    handlePasswordChange(e) {
        this.setState({
            inputPassword: e.target.value
        });
    }

    handleOfficeChange(e) {
        this.setState({
            selectedOffice: e.value
        })
    }

    handleRoleChange(e) {
        this.setState({
            selectedRole: e.value
        })
    }

    render() {
        const { inputName, inputSurname, inputEmail, inputPassword, inputOffice } = this.state;
        const { t } = this.props;
        return (
            <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 pt-4`}>
                <form className={`form-signin`} onSubmit={this.handleSubmit}>
                    <div className="form-group mb-2">
                        <input type="text" id="inputFullName" className={`form-control`} placeholder={t("Name")}
                            required autoFocus name="inputName" value={inputName}
                            onChange={this.handleNameChange} />
                    </div>
                    <div className="form-group mb-2">
                        <input type="text" id="inputFullName" className={`form-control`} placeholder={t("Surname")}
                            required name="inputSurname" value={inputSurname}
                            onChange={this.handleSurnameChange} />
                    </div>
                    <div className="form-group mb-2">
                        <input type="email" id="inputEmail" className={`form-control`} placeholder={t("Email")}
                            required name="inputEmail" value={inputEmail}
                            onChange={this.handleEmailChange} />
                    </div>
                    <div className="form-group" hidden={this.state.isEditing}>
                        <input type="password" id="inputPassword" className={`form-control`} placeholder={t("Password")}
                            required name="inputPassword" value={inputPassword}
                            onChange={this.handlePasswordChange} />
                    </div>
                    <div className="form-group mb-2">
                        <Select
                            options={this.state.officesOptions}
                            className="basic-multi-select"
                            placeholder={t("SelectOffice")}
                            onChange={this.handleOfficeChange}
                            required
                            value={this.state.officesOptions.filter(option => option.value === this.state.selectedOffice)}
                        />
                    </div>
                    <div className="form-group mb-2">
                        <Select
                            options={this.state.rolesOptions}
                            className="basic-multi-select"
                            placeholder={t("SelectEmployeeRole")}
                            onChange={this.handleRoleChange}
                            required
                            value={this.state.rolesOptions.filter(option => option.value === this.state.selectedRole)}
                        />
                    </div>
                    <button hidden={this.state.isEditing} className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SignUp")}</button>
                    <button hidden={!this.state.isEditing} className={`btn btn-lg btn-primary btn-block`} onClick={this.updateUser}>{t("EditEmployeeInfo")}</button>
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
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(SignUpPage));