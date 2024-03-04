import { memo } from "react";
import type { FC, ReactNode } from "react";
import classes from "../../styles/Titles/TitleForm.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    text?: ReactNode;
    children?: React.ReactNode;
}

const TitleForm: FC<Props> = memo(function TitleForm({ children, className }: Props) {
	return (
		<div className={`${resets.storybrainResets} ${classes?.root || ""} ${className || ""}`}>
			<div className={classes?.rectangle1}>
				<div className={classes.pAPERKILLER}>{children}</div>
			</div>
		</div>
	);
});


export default TitleForm;

