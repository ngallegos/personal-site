import { createContext } from "react";
import { SiteMetaData } from "../model/sitemetadata";

export const MetaContext = createContext({} as SiteMetaData)