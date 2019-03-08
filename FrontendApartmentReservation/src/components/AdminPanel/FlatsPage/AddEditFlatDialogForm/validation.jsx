import { SubmissionError } from 'redux-form';

export function typeValidation(values) {
    let errors = {};

    let cityRegex = /^[a-zA-Z\u0080-\u024F]+(?:([\ \-\']|(\.\ ))[a-zA-Z\u0080-\u024F]+)*$/u;
    let numberRegex = /^[0-9]*$/g;
    let doubleNumberRegex = /^(?=.)([+-]?([0-9]*)(\.([0-9]+))?)$/;

    if(values['city'] !== undefined && !values['city'].match(cityRegex)) {
        errors.city = "Wrong City format";
    }

    if(values['guest_amount'] !== undefined && !values['guest_amount'].toString().match(numberRegex)) {
        errors.guest_amount = "Wrong Guest Amount format";
    }

    if(values['bedrooms_amount'] !== undefined && !values['bedrooms_amount'].toString().match(numberRegex)) {
        errors.bedrooms_amount = "Wrong Bedrooms Amount format";
    }

    if(values['area'] !== undefined && !values['area'].toString().match(doubleNumberRegex)) {
        errors.area = "Wrong Area format";
    }

    if(values['daily_price'] !== undefined && !values['daily_price'].toString().match(doubleNumberRegex)) {
        errors.daily_price = "Wrong Daily Price format";
    }

    return errors;
};

export function submitValidation(values) {
    if(!values['address']) {
        throw new SubmissionError({
            address: "Address value required.",
            _error: "Address value required."
        });
    }

    if(!values['city']) {
        throw new SubmissionError({
            city: "City value required.",
            _error: "City value required."
        });
    }

    if(!values['guest_amount']) {
        throw new SubmissionError({
            guest_amount: "Guest Amount value required.",
            _error: "Guest amount value required."
        });
    }

    if(!values['bedrooms_amount']) {
        throw new SubmissionError({
            bedrooms_amount: "Bedrooms Amount value required.",
            _error: "Bedrooms Amount value required."
        });
    }

    if(!values['area']) {
        throw new SubmissionError({
            area: "Area value required.",
            _error: "Area value required."
        });
    }

    if(!values['daily_price']) {
        throw new SubmissionError({
            daily_price: "Daily Price value required.",
            _error: "Daily Price value required."
        });
    }

    this.props.onSubmit(values);
    this.props.handleClose();
};