import { memo } from "react";
import type { FC, ReactNode } from "react";

import classes from "../../styles/Buttons/UtilButton.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    classes?: {
        root?: string;
    };
    onClick?: (event: React.FormEvent) => Promise<void>;
    children?: ReactNode;
}

const UtilButton: FC<Props> = memo(function UtilButton({ onClick, children, ...props }) {
	return (
		<button
			onClick={onClick}
			className={`${resets.storybrainResets} ${props.classes?.root || ""} ${props.className || ""} ${classes.root}`}>
			<div className={classes.rectangle}></div>
			<div className={`${classes.unnamed} ${classes.text}`}>{children}</div>
		</button>
	);
});

export default UtilButton;
