pid_file = "/vault/Project/vault-agent/pidfile"

vault {
  address = "http://vault:8200"
}

auto_auth {
  method {
    type = "approle"
    config = {
      role_id_file_path                   = "/vault/Project/vault-agent/role-id"
      secret_id_file_path                 = "/vault/Project/vault-agent/secret-id"
      remove_secret_id_file_after_reading = false
    }
  }

  sink {
    type = "file"

    config = {
      path = "/vault/Project/vault-agent/token"
    }
  }
}

template {
  source      = "/vault/Project/vault-agent/appsettings.ctmpl"
  destination = "/vault/Project/appsettings.json"
}
