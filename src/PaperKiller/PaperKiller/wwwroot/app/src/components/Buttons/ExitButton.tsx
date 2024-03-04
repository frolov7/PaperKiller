import { memo } from "react";
import type { FC } from "react";

import classes from "../../styles/Buttons/BackToMenuButton.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    classes?: {
        root?: string;
    };
    onClick?: () => void; 
}

const ExitButton: FC<Props> = memo(function BackToMenuButton({ onClick, ...props }) {
	return (
		<div className={`${resets.storybrainResets} ${props.classes?.root || ""} ${props.className || ""} ${classes.root}`} onClick={onClick}>
			<div className={classes.rectangle}></div>
			<div className={classes.frame6}>
				<div className={classes.buttonName}>Выйти</div>
			</div>
		</div>
	);
});

export default ExitButton;
