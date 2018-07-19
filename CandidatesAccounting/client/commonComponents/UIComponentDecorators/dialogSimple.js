import React from 'react'
import PropTypes from 'prop-types'
import Slide from '@material-ui/core/Slide'
import Dialog from '@material-ui/core/Dialog'
import DialogActions from '@material-ui/core/DialogActions'
import DialogContent from '@material-ui/core/DialogContent'
import DialogTitle from '@material-ui/core/DialogTitle'

const Transition = (props) => <Slide direction='up' {...props} />

const SimpleDialog = (props) => {
  return (
    <Dialog
      open={props.isOpen}
      TransitionComponent={Transition}
      disableBackdropClick
    >
      <DialogTitle>{props.title}</DialogTitle>
      <DialogContent>
        {props.children}
      </DialogContent>
      <DialogActions>
        {props.actions}
      </DialogActions>
    </Dialog>
  )
}

SimpleDialog.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  onRequestClose: PropTypes.func.isRequired,
  title: PropTypes.string,
  actions: PropTypes.object,
}

export default SimpleDialog