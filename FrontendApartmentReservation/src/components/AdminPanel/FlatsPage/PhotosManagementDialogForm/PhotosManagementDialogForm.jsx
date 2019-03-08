import React from 'react';
import { Dialog, DialogTitle, DialogContent, IconButton, Button, Modal } from '@material-ui/core';
import { reduxForm } from 'redux-form';
import { Close as CloseIcon } from '@material-ui/icons';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';

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
    },
    fileInput: {
        float: "left",
        width: '30vh'
    },
    paper: {
        top: 70,
        left: 340,
        position: 'absolute',
    }
});

class PhotosManagementDialogForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            open: true,
            files: [],
            photo_code: '',
            openBigImg: false,
            photo: ''
        };

        this.onFileSelected = this.onFileSelected.bind(this);
        this.handleBigImgOpen = this.handleBigImgOpen.bind(this);
        this.handleBigImgClose = this.handleBigImgClose.bind(this);
    }

    handleBigImgOpen = (photo) => {
        this.setState({
            openBigImg: true,
            photo: photo
        });
    };

    handleBigImgClose = () => {
        this.setState({
            openBigImg: false,
            photo: ''
        });
    };

    onFileSelected(event) {
        var selectedFile = event.target.files[0];
        var reader = new FileReader();

        reader.onload = function(event) {
            this.setState({
                photo_code: event.target.result
            });
        }.bind(this);

        reader.readAsDataURL(selectedFile);
    }

    render() {
        const { classes, photos } = this.props;

        let photosMapped = photos.map((photo, index) => {
            return <div className="row" key={index}>
                <div className="col-6">
                    <Button onClick={() => this.handleBigImgOpen(photo.photo_code)}>
                        <img width='200vh' height='200vh' src={photo.photo_code} />
                    </Button>
                </div>
                <div className="col-6">
                    <Button variant="contained" color="secondary" onClick={() => this.props.removePhoto(photo.id, photo.flat_id)}>
                        REMOVE
                    </Button>
                </div>
            </div>
        });

        return (
            <React.Fragment>
                <Dialog open={this.state.open}
                        onClose={this.props.handleClose}
                        fullWidth={true}
                >
                    <form onSubmit={(event) => this.props.handleSubmit(this.state.photo_code, event)}>
                        <DialogTitle>
                            <span className={classes.formTitle}>{'PHOTO MANAGEMENT'}</span>
                            <IconButton className={classes.exitButton} onClick={this.props.handleClose}>
                                <CloseIcon className={classes.closeIcon} />
                            </IconButton>
                        </DialogTitle>
                        <DialogContent>
                            <div style={{overflowwY: 'scroll', height: '60vh'}}>
                                {photosMapped}
                            </div>
                        </DialogContent>
                        <div className="container">
                            <div className="row">
                                <div className="col-5 pt-2">
                                    <input type="file" className={classes.fileInput} onChange={this.onFileSelected} />
                                </div>
                                <div className="col-4">
                                    <Button disabled={this.state.photoCode === ''} type="submit" variant="raised" color="primary" className={classes.submitButton}>
                                        ADD
                                    </Button>
                                </div>
                                <div className="col-3">
                                    <Button variant="raised" className={classes.closeButton} onClick={this.props.handleClose}>
                                        Close
                                    </Button>
                                </div>
                            </div>
                        </div>
                    </form>
                </Dialog>>
                {this.state.openBigImg && this.renderBigPhotoModal(this.state.photo, classes)}
            </React.Fragment>
        );
    }

    renderBigPhotoModal(photo, classes) {
        return (
            <Modal
                open={this.state.openBigImg}
                onClose={this.handleBigImgClose}
                className={classes.paper}
            >
                <img src={photo} className={classes.imgBig} />
            </Modal>
        );
    }
}

PhotosManagementDialogForm.propTypes = {
    classes: PropTypes.object.isRequired
};

export default reduxForm({
    form: 'PhotosManagementDialogForm'
})(withStyles(styles)(PhotosManagementDialogForm));