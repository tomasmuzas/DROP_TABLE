import React from 'react';
import { Link } from 'react-router-dom';

export default class PageHeader extends React.Component {
    render() {
        return (
            <div>
                <nav className="navbar navbar-expand navbar-dark bg-dark p-0">
                        <ul className="pr-2 navbar-nav mr-auto">
                        <li className="nav-item pt-1 robot "> <LinkButton link="/apartments" name="Apartments" /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto">
                            <li className="nav-item pt-1 robot "> <LinkButton link="/authentication" name="Authentication" /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto">
                            <li className="nav-item pt-1 robot "> <LinkButton link="/trips" name="Trips" /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto">
                            <li className="nav-item pt-1 robot "> <LinkButton link="/users" name="Users" /></li>
                        </ul>
                        <ul className="pr-2 navbar-nav mr-auto">
                            <li className="nav-item pt-1 robot "> <LinkButton link="/signUp" name="Sign Up!" /></li>
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
