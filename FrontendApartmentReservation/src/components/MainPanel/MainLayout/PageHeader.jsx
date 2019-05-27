import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import * as actionCreators from '../../../actions';
import { bindActionCreators } from 'redux';
import Switch from '@material-ui/core/Switch';


class PageHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            checked: true,
            collapsedButton: 'collapsed',
            collapsedAria: 'false',
            collapseDiv: '',
        }
        this.handleCollapsed = this.handleCollapsed.bind(this);
        this.logoutClick = this.logoutClick.bind(this);
    }

    handleCollapsed() {
        this.setState({
            collapsedButton: this.state.collapsedButton === '' ? 'collapsed' : '',
            collapsedAria: this.state.collapsedAria === 'true' ? 'false' : 'true',
            collapseDiv: this.state.collapseDiv === 'show' ? '' : 'show'
        });
    }
    handleChange = () => event => {
        this.setState({
            checked: event.target.checked
        });
        if (this.state.checked) {
            this.props.i18n.changeLanguage('lt');
        }
        else {
            this.props.i18n.changeLanguage('en');
        }
    };

    logoutClick(){
        this.props.logoutUser();
        this.setState({

        });
    }

    render() {
        const { t, i18n } = this.props;
        const isLoggedIn = sessionStorage.getItem("token") === null? false: true;
        const visibleForAdmin = (sessionStorage.getItem("token") !== null && sessionStorage.getItem("role") === 'Admin')?true:false;
        const visibleForOrganizer = (sessionStorage.getItem("token") !== null && (sessionStorage.getItem("role") === 'Organizer' || sessionStorage.getItem("role") === 'Admin'))?true:false;
        return (
            <div>
                <nav className="navbar navbar-collapse navbar-expand-lg navbar-dark bg-dark">
                    <button className={"navbar-toggler " + this.state.collapsedButton}
                        type="button"
                        data-toggle="collapse"
                        data-target="#navbarColor01"
                        aria-controls="navbarColor01"
                        aria-expanded={this.state.collapsedAria}
                        aria-label="Toggle navigation"
                        onClick={this.handleCollapsed}>
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className={" navbar-collapse collapse" + this.state.collapseDiv} id="navbarColor01">
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!visibleForAdmin}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/apartments" name={t("Apartments")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!visibleForOrganizer}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/trips" name={t("Trips")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!isLoggedIn}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/myInfo/myTrips" name={t("MyTrips")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!visibleForAdmin}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/signUp" name={t("SignUp")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {isLoggedIn}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/login" name={t("Login")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!visibleForOrganizer}>
                            <li className="nav-item pt-1 robot "> <LinkButton link="/createTrip" name={t("CreateTrip")} /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto" hidden = {!isLoggedIn}>
                            <li className="nav-item pt-1 robot text-white nav-link"> <a className="nav-link text-white" href="#" onClick={this.logoutClick}> {t("Logout")}</a> </li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto">
                            <li className="nav-item pt-1 robot "> <label style={{ color: 'white' }}> LT </label> <Switch
                                checked={this.state.checked}
                                onChange={this.handleChange()}
                            /><label style={{ color: 'white' }}> EN </label> </li>
                        </ul>
                    </div>
                </nav>
            </div>
        );
    }
}

const LinkButton = (props) => {
    const { link, name } = props;
    return <Link className={"nav-link text-white"} to={link}>{name}</Link>;
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

export default withTranslation()(connect(mapDispatchToProps, mapDispatchToProps)(PageHeader));