import React from 'react';
import { push } from 'react-router-redux';
import { connect } from 'react-redux';
import { Route, Redirect } from 'react-router';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class LoginPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputEmail: '',
            inputPassword: '',
            success: false
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        const { inputEmail, inputPassword } = this.state;
        this.props.login(inputEmail, inputPassword);
        this.setState({
            success: true
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

    render() {
        const { inputEmail, inputPassword } = this.state;
        const { t } = this.props;
        if (sessionStorage.getItem('token') === null) {
            return (
                <div className={`loginForm text-center jumbotron mx-auto col-12 col-lg-6 pb-1 pt-4`}>
                    <form className={`form-signin`} onSubmit={this.handleSubmit}>
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

                        <button className={`btn btn-lg btn-primary btn-block`} type="submit">{t("Login")}</button>
                    </form>
                </div>
            );
        }

        else {
            return (
                <Redirect to='/myInfo/myTrips' />
            );

        }
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(LoginPage));