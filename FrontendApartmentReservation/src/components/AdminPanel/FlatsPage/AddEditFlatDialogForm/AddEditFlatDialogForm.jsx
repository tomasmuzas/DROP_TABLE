import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { Dialog, DialogTitle, DialogContent, IconButton, Input, Button } from '@material-ui/core';
import { Close as CloseIcon } from '@material-ui/icons';
import { Field, reduxForm } from 'redux-form';
import { typeValidation, submitValidation } from './validation';

const styles = ({
    formTitle: {
        fontWeight: 'bold'
    },
    exitButton: {
        float: 'right',
        marginTop: 0
    },
    closeIcon: {
        fontSize: '34px'
    },
    formContent: {
        textAlign: 'center'
    },
    submitButton: {
        width: "140px",
        height: "40px",
        margin: "5% 2%",
        float: "left",
        margin: "2%"
    },
    closeButton: {
        width: "70px",
        height: "40px",
        margin: "5% 2%",
        fontSize: "12px",
        float: "left",
        margin: "2%"
    },
    errorMessage: {
        color: 'red',
        fontWeight: 'bold'
    }
});

const renderInputStyles = () => {
    return {
        border: "1px solid #DADFE1",
        borderRadius: "5px",
        height: "40px",
        width: "91.5%",
        paddingLeft: "10px"
    };
};

let initialValues = {};

class AddEditFlatDialogForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: true,
        };

        this.submitValidation = submitValidation.bind(this);
    }

    render() {
        const { classes, formTitle, buttonText, initialValues } = this.props;

        return (
            <Dialog open={this.state.open}
                    onClose={this.props.handleClose}
                    fullWidth={true}
            >
                <form onSubmit={this.props.handleSubmit(this.submitValidation)}>
                    <DialogTitle>
                        <span className={classes.formTitle}>{formTitle}</span>
                        <IconButton className={classes.exitButton} onClick={this.props.handleClose}>
                            <CloseIcon className={classes.closeIcon} />
                        </IconButton>
                    </DialogTitle>
                    <DialogContent>
                       <div className={classes.formContent}>
                            <Field
                                name="address"
                                component={this.renderInputBox}
                                label="Address"
                                style={renderInputStyles()}
                                inputText={initialValues.address}
                                errorMessage={classes.errorMessage}
                            />
                            <Field
                                name="city"
                                component={this.renderInputBox}
                                label="City"
                                style={renderInputStyles()}
                                inputText={initialValues.city}
                                errorMessage={classes.errorMessage}
                           />
                            <Field
                                name="guest_amount"
                                component={this.renderInputBox}
                                label="Guest Amount"
                                style={renderInputStyles()}
                                inputText={initialValues.guest_amount}
                                errorMessage={classes.errorMessage}
                            />
                            <Field
                                name="bedrooms_amount"
                                component={this.renderInputBox}
                                label="Bedrooms Amount"
                                style={renderInputStyles()}
                                inputText={initialValues.bedrooms_amount}
                                errorMessage={classes.errorMessage}
                            />
                            <Field
                                name="area"
                                component={this.renderInputBox}
                                label="Area"
                                style={renderInputStyles()}
                                inputText={initialValues.area}
                                errorMessage={classes.errorMessage}
                            />
                            <Field
                                name="daily_price"
                                component={this.renderInputBox}
                                label="Daily Price"
                                style={renderInputStyles()}
                                inputText={initialValues.daily_price}
                                errorMessage={classes.errorMessage}
                            />
                        </div>
                        <Button type="submit" variant="raised" color="primary" className={classes.submitButton}>
                            {buttonText}
                        </Button>
                        <Button variant="raised" className={classes.closeButton} onClick={this.props.handleClose}>
                            Close
                       </Button>
                    </DialogContent>
                </form>
            </Dialog>
        );
    }

    renderInputBox = (props) => {
        delete props.input.value;
        return(
            <div>
                {props.meta.touched && props.meta.error && <span className={props.errorMessage}>{props.meta.error}</span>}
                <div
                    ><b>
                        <span style={{color: 'red'}}>*</span>
                        {props.label}
                    </b>
                </div>
                <Input
                    style={props.style}
                    placeholder={props.placeholder}
                    className={props.className}
                    {...props.input}
                    error={props.meta.invalid}
                    defaultValue={props.inputText}
                />
            </div>
        );
    }
}

AddEditFlatDialogForm.propTypes = {
    classes: PropTypes.object.isRequired
};

export default reduxForm({
    form: 'AddEditFlatDialogForm',
    initialValues: initialValues,
    validate: typeValidation
})(withStyles(styles)(AddEditFlatDialogForm));