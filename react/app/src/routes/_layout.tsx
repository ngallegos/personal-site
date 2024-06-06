import { getSiteMetadata } from '../util/contentUtil';
import { Link, Outlet, useLoaderData } from 'react-router-dom';
import { SiteMetaData } from '../model/sitemetadata';
import './site.css';
import ReactMarkdown from 'react-markdown';
import { Helmet } from 'react-helmet';
import rehypeRaw from 'rehype-raw';
import { MetaContext } from '../context/metaContext';


function Layout() {
  const meta = useLoaderData() as SiteMetaData;
  document.body.classList.add("dark-theme");
  document.body.classList.add("font-sans");
  document.body.classList.add("quicksand");

  return (
    <MetaContext.Provider value={meta}>
      <div>
        <Helmet>
          {meta.headMetaData.map((meta, index) => (
            <meta key={index} name={meta.name} content={meta.content} />
          ))}
          </Helmet>
        <div className="p-6 sm:p-10 md:p-16 flex flex-wrap">
          <div className="w-full md:w-1/3 md:pr-32 order-3 md:order-1">
              <div className="max-w-2xl md:float-right md:text-right leading-loose tracking-tight md:sticky md:top-0 ">
                  <p className="font-bold my-4 md:my-12">Things To See</p>
                  <ul className="flex flex-wrap justify-between flex-col">
                    {meta.navLinks.map((link, index) => (
                      <li key={index}>
                        {!!link.external ? <a className="nav" href={link.slug} target="_blank" rel="noreferrer">{link.text}</a>
                        : <Link className="nav" to={link.slug}>{link.text}</Link>}
                      </li>
                    ))}
                  </ul>
              </div>
          </div>
          <div className="w-full md:w-2/3 order-1 md:order-2">
              <div className="content max-w-2xl leading-loose tracking-tight">
                  <Outlet/>
              </div>
          </div>
          <div className="w-full md:w-1/3 md:pr-32 pt-12 md:pt-0 md:sticky md:bottom-0 order-4 md:order-3">
              <div className="max-w-2xl md:float-right md:text-right leading-loose tracking-tight md:mb-16">
                  <p className="font-bold my-4 md:my-12">Places To Go</p>
                  <ul className="flex flex-wrap justify-between flex-row md:flex-col">
                    {meta.contactLinks.map((link, index) => (
                      <li key={index}>
                        {!!link.external ? <a className="nav mx-2 md:mx-0" href={link.slug} target="_blank" rel="noreferrer">{link.text}</a>
                        : <Link className="nav mx-2 md:mx-0" to={link.slug}>{link.text}</Link>}
                      </li>
                    ))}
                  </ul>
              </div>
          </div>
          <div className="w-full md:w-2/3 order-2 md:order-4">
              <div className="max-w-2xl leading-loose tracking-tight">
                  {!!meta.aboutMe && (
                    <>
                      <p className="font-bold my-4 md:my-12">About Me</p>
                      <ReactMarkdown rehypePlugins={rehypeRaw as any}>{meta.aboutMe}</ReactMarkdown>
                    </>)}
              </div>
          </div>
        </div>
      </div>
    </MetaContext.Provider>
  );
}

export async function loader({ params }: any){
  const content = await getSiteMetadata();
  if (!content) throw new Response("", { status: 404 });
  return content;
}

export default Layout;
