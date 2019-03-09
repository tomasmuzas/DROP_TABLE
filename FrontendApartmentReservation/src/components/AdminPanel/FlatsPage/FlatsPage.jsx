import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { Table, TableHead, TableBody, TableRow, TableCell, Button, Modal } from '@material-ui/core';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actionCreators from '../../../actions';
import AddEditFlatDialogForm from './AddEditFlatDialogForm/AddEditFlatDialogForm';
import PhotosManagementDialogForm from './PhotosManagementDialogForm/PhotosManagementDialogForm';

const styles = ({
    img: {
        width: '20px',
        height: '20px'
    },
    flatCreateButton : {
        float: 'right'
    },
    flatButtons: {
        width: '100px',
        margin: '0px'
    },
    imgBig: {
        width: '800px',
        height: '600px'
    },
    paper: {
        top: 100,
        left: 340,
        position: 'absolute',
    }
});

class FlatsPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            openBigImg: false,
            photo: '',
            flatAddEditOpen: false,
            editFlat: '',
            photoManagementOpen: false,
            photos: [],
            flatToAddPhoto: ''
        };

        this.handleBigImgOpen = this.handleBigImgOpen.bind(this);
        this.handleBigImgClose = this.handleBigImgClose.bind(this);
        this.handleAddFlatOpen = this.handleAddFlatOpen.bind(this);
        this.handleAddFlatClose = this.handleAddFlatClose.bind(this);
        this.handleDialogSubmit = this.handleDialogSubmit.bind(this);
        this.handleRemoveFlat = this.handleRemoveFlat.bind(this);
        this.handleFlatEdit = this.handleFlatEdit.bind(this);
        this.handlePhotosManagementOpen = this.handlePhotosManagementOpen.bind(this);
        this.handlePhotosManagementClose = this.handlePhotosManagementClose.bind(this);
        this.handlePhotosManagementSubmit = this.handlePhotosManagementSubmit.bind(this);
    }

    componentDidMount() {
        this.props.getAllFlats();
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

    handleAddFlatOpen = () => {
        this.setState({
            flatAddEditOpen: true
        });
    }

    handleAddFlatClose = () => {
        this.setState({
            flatAddEditOpen: false,
            editFlat: false
        });
    }

    handleFlatEdit = (flat) => {
        this.setState({
            flatAddEditOpen: true,
            editFlat: flat
        });
    }

    handleDialogSubmit(values) {
        if(this.state.editFlat) {
            values.id = this.state.editFlat.id;
            this.props.editFlat(values);
        } else {
            this.props.addFlat(values);
        }

        this.setState({
            flatAddEditOpen: false,
            editFlat: false
        });
    }

    handleRemoveFlat(id) {
        this.props.removeFlat(id);
    }

    handlePhotosManagementOpen = (flat) => {
        this.setState({
            photoManagementOpen: true,
            flatToAddPhoto: flat
        })
    }

    handlePhotosManagementClose = () => {
        this.setState({
            photoManagementOpen: false,
            flatToAddPhoto: ''
        })
    }

    handlePhotosManagementSubmit = (photoCode, event) => {
        event.preventDefault();
        this.props.addPhoto(this.state.flatToAddPhoto.id, photoCode);
        this.handlePhotosManagementClose();
    };

    render() {
        const { flats, classes } = this.props;
        let flatsMapped = flats && flats.map((flat, index) => {
            return (
                <TableRow key={index}>
                    <TableCell>{flat.city}</TableCell>
                    <TableCell>{flat.address}</TableCell>
                    <TableCell>{flat.guest_amount}</TableCell>
                    <TableCell>{flat.bedrooms_amount}</TableCell>
                    <TableCell>{flat.area}</TableCell>
                    <TableCell>{flat.daily_price}</TableCell>
                    <TableCell>
                        {flat.photos && flat.photos.map((photo, photoIndex) => {
                            return (
                                <Button key={index + ' ' + photoIndex} onClick={() => this.handleBigImgOpen(photo.photo_code)}>
                                    <img className={classes.img} src={photo.photo_code} />
                                </Button>
                            );
                        })}
                    </TableCell>
                    <TableCell>
                        <Button
                            variant="contained"
                            color="primary"
                            className={classes.flatButtons}
                            onClick={() => this.handleFlatEdit(flat)}
                        >
                            EDIT
                        </Button>
                        <Button
                            variant="contained"
                            color="secondary"
                            className={classes.flatButtons}
                            onClick={() => this.handleRemoveFlat(flat.id)}
                        >
                            REMOVE
                        </Button>
                        <Button variant="contained"
                                color="inherit"
                                className={classes.flatButtons}
                                onClick={() => this.handlePhotosManagementOpen(flat)}>
                            MANAGE PHOTOS
                        </Button>
                    </TableCell>
                </TableRow>
            );
        });

        return (
            <div>
                <Button variant="contained"
                        color="primary"
                        className={classes.flatCreateButton}
                        onClick={this.handleAddFlatOpen}
                >
                    CREATE NEW FLAT
                </Button>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>City</TableCell>
                            <TableCell>Address</TableCell>
                            <TableCell>Guest Amount</TableCell>
                            <TableCell>Bedrooms Amount</TableCell>
                            <TableCell>Area</TableCell>
                            <TableCell>Daily Price</TableCell>
                            <TableCell>Photos</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {flatsMapped}
                    </TableBody>
                </Table>
                {this.state.openBigImg && this.renderBigPhotoModal(this.state.photo, classes)}
                {this.state.flatAddEditOpen && this.renderAddEditDialog()}
                {this.state.photoManagementOpen &&
                    <PhotosManagementDialogForm
                        handleClose={this.handlePhotosManagementClose}
                        handleSubmit={this.handlePhotosManagementSubmit}
                        photos={this.state.flatToAddPhoto.photos}
                        removePhoto={this.props.removePhoto}
                    />
                }
            </div>
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

    renderAddEditDialog() {
        let title = this.state.editFlat ? "EDIT FLAT" : "ADD NEW FLAT";
        let buttonText = this.state.editFlat ? "SAVE" : "ADD";

        if (this.state.flatAddEditOpen) {
            return <AddEditFlatDialogForm
                onSubmit={this.handleDialogSubmit}
                handleClose={this.handleAddFlatClose}
                initialValues={this.state.editFlat ? this.state.editFlat : {}}
                formTitle={title}
                buttonText={buttonText}
            />
        }
    }
}

FlatsPage.propTypes = {
    classes: PropTypes.object.isRequired
};

const mapDispatchToProps = (dispatch) => {
return bindActionCreators(actionCreators, dispatch);
}

const mapStateToProps = (state) => {
    return {
        flats: state.flats
    };
}

export default withStyles(styles)(connect(mapStateToProps, mapDispatchToProps)(FlatsPage));