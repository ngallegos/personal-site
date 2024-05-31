#create an S3 bucket for the static site
resource "aws_s3_bucket" "static_site" {
    bucket = "${var.site_name}-personal-site-react-s3-${var.env}"
}

# make things public so we can host a static site
resource "aws_s3_bucket_public_access_block" "allow_public_access" {
    bucket = aws_s3_bucket.static_site.id
    block_public_acls       = false
    block_public_policy     = false
    ignore_public_acls      = false
    restrict_public_buckets = false
}

#configure the static site hosting from s3
resource "aws_s3_bucket_website_configuration" "static_site" {
    bucket = aws_s3_bucket.static_site.id
    index_document { 
        suffix = "index.html"
    }
    error_document { 
        key = "index.html" 
    }
}

resource "aws_s3_bucket_policy" "allow_public_access" {
    bucket = aws_s3_bucket.static_site.id
    policy = data.aws_iam_policy_document.static_site_bucket_policy.json
    depends_on = [
        aws_s3_bucket_website_configuration.static_site
    ]
}

# #create the static site bucket policy
data "aws_iam_policy_document" "static_site_bucket_policy" {
    statement {
        actions = [
            "s3:GetObject"
        ]
        resources = [
            "${aws_s3_bucket.static_site.arn}/*"
        ]
        principals {
            type = "AWS"
            identifiers = [
                "*"
            ]
        }
    }
}