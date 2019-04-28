import React from 'react';

import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class SignUpPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputName: '',
            inputSurname: '',
            inputEmail: '',
            inputPassword: '',
            inputOffice: '',
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleSurnameChange = this.handleSurnameChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleOfficeChange = this.handleOfficeChange.bind(this);

    }

    handleSubmit(e) {
        e.preventDefault();
        const { inputName, inputSurname, inputEmail, inputPassword, inputOffice } = this.state;
        const regex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,15}$");
        var regexTest = regex.test(inputPassword);
        if (regexTest) {
            this.props.signUpUser(inputName, inputSurname, inputEmail, inputPassword, inputOffice);
        } else {
            alert(this.props.t("PasswordError"));
        }
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
            inputOffice: e.target.value
        });
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
                    <div className="form-group">
                        <input type="password" id="inputPassword" className={`form-control`} placeholder={t("Password")}
                            required name="inputPassword" value={inputPassword}
                            onChange={this.handlePasswordChange} />
                    </div>
                    <div className="form-group">
                        <input type="text" id="inputOffice" className={`form-control`} placeholder={t("Office")}
                            required name="inputOffice" value={inputOffice}
                            onChange={this.handleOfficeChange} />
                    </div>
                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("SignUp")}</button>
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
        user: state.user
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(SignUpPage));