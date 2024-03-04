import React, { memo, FC } from "react";
import { StudentDTO } from "../../interfaces/StudentDTO";
import classes from "../../styles/Tables/StudentDataTable.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    classes?: {
        tableGroup?: string;
    };
    student: StudentDTO;
}

const StudentTable: FC<Props> = memo(function StudentDataTable({ student, ...props }) {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`}>
			<div className={classes.frameName}>
				<div className={classes.unnamed2}>Имя:</div>
			</div>
			<div className={classes.frameSurname}>
				<div className={classes.unnamed3}>Фамилия:</div>
			</div>
			<div className={classes.framePhone}>
				<div className={classes.unnamed4}>Телефон:</div>
			</div>
			<div className={classes.frameStudak}>
				<div className={classes.unnamed5}>Студенческий:</div>
			</div>
			<div className={classes.frameRoom}>
				<div className={classes.unnamed6}>Номер комнаты:</div>
			</div>
			<div className={classes.frameRoomEx}>
				<div className={classes.rNumber}>{student.roomNumber}</div>
			</div>
			<div className={classes.frameStudakEx}>
				<div className={classes.studentID}>{student.studak}</div>
			</div>
			<div className={classes.framePhoneEx}>
				<div className={classes.pNumber}>{student.phoneNumber}</div>
			</div>
			<div className={classes.frameSurnameEx}>
				<div className={classes.surname}>{student.surname}</div>
			</div>
			<div className={classes.frameNameEx}>
				<div className={classes.name}>{student.name}</div>
			</div>
		</div>
	);
});

export default StudentTable;

