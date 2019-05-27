import React from 'react';
import './style.css';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';

class LoginPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputEmail: '',
            inputPassword: ''
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        const { inputEmail, inputPassword } = this.state;
        this.props.loginUser(inputEmail, inputPassword);
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
        return (
            <div className={`loginForm text-center jumbotron mx-auto col-6 pb-1 pt-4`}>
                <form className={`form-signin`} onSubmit={this.handleSubmit}>
                    <div className="form-group mb-2">
                        <label htmlFor="inputEmail" className={`sr-only`}>Email address</label>
                        <input type="email" id="inputEmail" className={`form-control`} placeholder="Email address"
                            required autoFocus name="inputEmail" value={inputEmail}
                            onChange={this.handleEmailChange} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="inputPassword" className={`sr-only`}>Password</label>
                        <input type="password" id="inputPassword" className={`form-control`} placeholder="Password"
                            required name="inputPassword" value={inputPassword}
                            onChange={this.handlePasswordChange} />
                    </div>
                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">Login</button>
                </form>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (store) => {
    return {
        user: store.user
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginPage);