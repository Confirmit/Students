import React from 'react';
import {CreateCandidate} from '../candidates/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import EditCandidateForm from './editCandidateForm';

export default function AddCandidateDialog(props) {
  return (
    <div className="add-btn float-right">
      <DialogWindow
        content={
          <EditCandidateForm
            additionMode={true}
            changeTempCandidateInfo={props.changeTempCandidateInfo}
            tempCandidate={props.tempCandidate}
          />
        }
        label="Add new candidate"
        openButtonType="fab"
        openButtonContent={<AddIcon/>}
        acceptButtonContent={<div className="button-content"><AddIcon/> <span style={{marginTop: 3}}>add</span></div>}
        open={function() {
          props.setTempCandidate(CreateCandidate(props.candidateType, {}));
        }}
        accept={function() {
          props.addCandidate(CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.setTempCandidate(CreateCandidate(props.candidateType, {}));
        }}
      />
    </div>);
}