import React from 'react';
import { Link } from 'react-router-dom';
import { withTranslation } from 'react-i18next';
import Switch from '@material-ui/core/Switch';


class PageHeader extends React.Component {
    state = {
        checked: true,
    }
    handleChange = () => event => {
        this.setState({
            checked: event.target.checked
        });
        if(this.state.checked){
            this.props.i18n.changeLanguage('lt');
        }
        else{
            this.props.i18n.changeLanguage('en');
        }
      };

    render() {
        const { t, i18n } = this.props;
        return (
            <div>
                <nav className="navbar navbar-expand navbar-dark bg-dark p-0">
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/apartments" name={t("Apartments")} /></li>
                    </ul>
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/authentication" name={t("Authentication")}  /></li>
                    </ul>
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/trips" name={t("Trips")} /></li>
                    </ul>
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/users" name={t("Users")} /></li>
                    </ul>
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/signUp" name={t("SignUp")} /></li>
                    </ul>
                    <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <label style={{ color: 'white' }}> LT </label> <Switch
                            checked={this.state.checked}
                            onChange={this.handleChange()}
                        /><label style={{ color: 'white' }}> EN </label> </li>
                    </ul>
                </nav>
            </div>
        );
    }
}

const LinkButton = (props) => {
    const { link, name } = props;
    return <Link className={"nav-link text-white"} to={link}>{name}</Link>;
};

export default withTranslation()(PageHeader);