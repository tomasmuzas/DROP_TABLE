import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { connect } from 'react-redux';
import { withTranslation } from 'react-i18next';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import { Link } from 'react-router-dom';
import i18n from "../../../i18n";
import Button from '@material-ui/core/Button';
import Icon from '@material-ui/icons/Edit';
import IconButton from '@material-ui/core/IconButton';

class UserCard extends React.Component {
    render() {
        const { t, employee } = this.props;

        return (
            <div className="row mt-5 mx-5" style={{ backgroundColor: '#eaecef', boxShadow: '1px 3px 1px #9E9E9E', borderRadius: "5pt" }}>
                <div className="col-6 justify-content-md-center pt-3 pb-3">
                    <h5>{employee.firstName} {employee.lastName}</h5>
                    <h6>{employee.role === 0 ? t('Regular') : employee.role === 1? t('Organizer') : employee.role === 2? t('Admin') : ''}</h6>
                </div>
                <div className="col-6 text-right" hidden={false}>
                    <Link to={'/signUp/' + employee.id}>
                        <IconButton className="EditIconButton">
                            <Icon>edit_icon</Icon>
                        </IconButton>
                    </Link>
                </div>
            </div>
        )

    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        trips: state.trips
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(UserCard));