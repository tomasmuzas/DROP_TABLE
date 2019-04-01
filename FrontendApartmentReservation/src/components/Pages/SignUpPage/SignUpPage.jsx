import React from 'react';

import { connect } from 'react-redux';
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
        const {inputName, inputSurname, inputEmail, inputPassword, inputOffice} = this.state;
        const regex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,15}$");
        var test = regex.test(inputPassword);
        if (test) {
        this.props.signUpUser(inputName, inputSurname, inputEmail, inputPassword, inputOffice);
        } else {
            alert('Password must contain one uppercase one lowercase, special symbol and be 5-15 chars length');
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
        const {inputName, inputSurname, inputEmail, inputPassword, inputOffice } = this.state;
        return (
            <div className={`loginForm text-center jumbotron mx-auto col-6 pb-1 pt-4`}>
                <form className={`form-signin`} onSubmit={this.handleSubmit}>
                    <div className="form-group mb-2">
                        <input type="text" id="inputFullName" className={`form-control`} placeholder="Name"
                            required autoFocus name="inputName" value={inputName}
                            onChange={this.handleNameChange} />
                    </div>
                    <div className="form-group mb-2">
                        <input type="text" id="inputFullName" className={`form-control`} placeholder="Surname"
                            required name="inputSurname" value={inputSurname}
                            onChange={this.handleSurnameChange} />
                    </div>
                    <div className="form-group mb-2">
                        <input type="email" id="inputEmail" className={`form-control`} placeholder="Email"
                            required name="inputEmail" value={inputEmail}
                            onChange={this.handleEmailChange} />
                    </div>
                    <div className="form-group">
                        <input type="password" id="inputPassword" className={`form-control`} placeholder="Password"
                            required name="inputPassword" value={inputPassword}
                            onChange={this.handlePasswordChange} />
                    </div>
                    <div className="form-group">
                        <input type="text" id="inputOffice" className={`form-control`} placeholder="Office"
                            required name="inputOffice" value={inputOffice}
                            onChange={this.handleOfficeChange} />
                    </div>
                    <button className={`btn btn-lg btn-primary btn-block`} type="submit">Sign up</button>
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

export default connect(mapStateToProps, mapDispatchToProps)(SignUpPage);