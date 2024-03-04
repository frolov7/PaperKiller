import { memo } from "react";
import type { FC } from "react";
import classes from "../../styles/Titles/TitleAppMain.module.css";
import resets from "../../styles/_resets.module.css";
interface Props {
    className?: string;
    classes?: {
        root?: string;
    };
}

const TitleAppMain: FC<Props> = memo(function TitleAppMain(props = {}) {
	return (
		<div className={`${resets.storybrainResets} ${props.classes?.root || ""} ${props.className || ""} ${classes.root}`}>
			<div className={classes.rectangle1}>
				<div className={classes.pAPERKILLER}>PAPERKILLER</div>
			</div>

		</div>
	);
});

export default TitleAppMain;