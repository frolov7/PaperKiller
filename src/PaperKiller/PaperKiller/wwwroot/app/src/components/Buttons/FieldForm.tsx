import React, { ChangeEvent } from "react";
import classes from "../../styles/Buttons/FieldForm.module.css";
import resets from "../../styles/_resets.module.css";

interface FieldFormProps {
    type: string;
    value: string;
    onChange: (event: ChangeEvent<HTMLInputElement>) => void;
    placeholder?: string;
    iconSrc?: string;
}

const FieldForm: React.FC<FieldFormProps> = ({ type, value, onChange, placeholder, iconSrc }) => {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`}>
			<div className={classes.line}></div>
			<div className={classes.icon}>
				{iconSrc && <img src={iconSrc} alt="Icon" className={classes.iconImage} />}
			</div>
			<input
				type={type}
				value={value}
				onChange={onChange}
				placeholder={placeholder}
				className={classes.inputText}
			/>
		</div>
	);
};

export default FieldForm;
