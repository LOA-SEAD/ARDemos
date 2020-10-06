package br.ufscar.sead.loa.scalear.remar

import br.ufscar.sead.loa.remar.api.MongoHelper
import grails.converters.JSON
import grails.plugin.springsecurity.annotation.Secured
import grails.util.Environment

import static org.springframework.http.HttpStatus.*
import grails.transaction.Transactional

@Secured(["isAuthenticated()"])
@Transactional(readOnly = true)
class Model3DController {

    static allowedMethods = [save: "POST", update: "PUT", delete: "DELETE"]

    def beforeInterceptor = [action: this.&check, only: ['index']]

    def springSecurityService

    private check() {
        if (springSecurityService.isLoggedIn())
            session.user = springSecurityService.currentUser
        else {
            log.debug "Logout: session.user is NULL !"
            session.user = null
            redirect controller: "login", action: "index"

            return false
        }
    }

    def index(Integer max) {
        if (params.t) {
            session.taskId = params.t
        }

        def list = Model3D.findAllByAuthor(session.user.username)

        render view: "index", model: [model3DList: list, model3DInstanceCount: list.size(),
                                      userName   : session.user.username, userId: session.user.id]

    }

    def create() {
        respond new Model3D(params)
    }

    @Transactional
    def newModel3D(Model3D model3DInstance) {
        if (model3DInstance.author == null) {
            model3DInstance.author = session.user.username
        }

        Model3D newModel = new Model3D();
        newModel.id = model3DInstance.id
        newModel.name = model3DInstance.name
        newModel.source = model3DInstance.source
        newModel.author = model3DInstance.author
        newModel.taskId = session.taskId as String
        newModel.ownerId = session.user.id

        newModel.save flush: true

        if (newModel.hasErrors()) {
            println newModel.errors
            respond newModel.errors, view: 'create' //TODO
            render newModel.errors;
            return
        }

        if (request.isXhr()) {
            render(contentType: "application/json") {
                JSON.parse("{\"id\":" + newModel.getId() + "}")
            }
        } else {
            // TODO
        }

        redirect(action: index())


    }

    @Transactional
    def save(Model3D model3DInstance) {
        if (model3DInstance == null) {
            notFound()
            return
        }

        model3DInstance.taskId = session.taskId as String

        model3DInstance.save flush: true

        if (model3DInstance.hasErrors()) {
            println model3DInstance.errors
            respond model3DInstance.errors, view: 'create' //TODO
            render model3DInstance.errors;
            return
        }

        if (request.isXhr()) {
            render(contentType: "application/json") {
                JSON.parse("{\"id\":" + model3DInstance.getId() + "}")
            }
        } else {
            // TODO
        }

        redirect(action: index())
    }

    def edit(Model3D model3DInstance) {
        respond model3DInstance
    }

    @Transactional
    def update() {

        Model3D model3DInstance = Model3D.findById(Integer.parseInt(params.modelID))

        model3DInstance.name = params.name
        model3DInstance.source = params.source
        model3DInstance.save flush: true

        redirect action: "index"
    }

    @Transactional
    def delete(Model3D model3DInstance) {
        if (model3DInstance == null) {
            notFound()
            return
        }

        model3DInstance.delete flush: true

        if (request.isXhr()) {
            render(contentType: "application/json") {
                JSON.parse("{\"id\":" + model3DInstance.getId() + "}")
            }
        } else {
            // TODO
        }
    }

    protected void notFound() {
        request.withFormat {
            form multipartForm {
                flash.message = message(code: 'default.not.found.message', args: [message(code: 'model.label', default: 'Model3D'), params.id])
                redirect action: "index", method: "GET"
            }
            '*' { render status: NOT_FOUND }
        }
    }

    def finish() {

        Model3D modelo = Model3D.findById(params.id)

        def dataPath = servletContext.getRealPath("/data")
        def userPath = new File(dataPath, "/" + springSecurityService.getCurrentUser().getId() + "/" + session.taskId)
        userPath.mkdirs()

        def fileName = "path.txt"
        File file = new File("$userPath/$fileName");

        def bw = new BufferedWriter(new OutputStreamWriter(
                new FileOutputStream(file), "UTF-8"))

        bw.write(modelo.source);

        bw.close();

        String id = MongoHelper.putFile(file.absolutePath)

        def port = request.serverPort
        if (Environment.current == Environment.DEVELOPMENT) {
            port = 8080
        }

        redirect uri: "http://${request.serverName}:${port}/process/task/complete/${session.taskId}", params: [files: id]
    }

    def returnInstance(Model3D model3DInstance) {

        if (model3DInstance == null) {
            notFound()
        } else {
            render model3DInstance.name + "%@!" +
                    model3DInstance.source + "%@!" +
                    model3DInstance.author + "%@!" +
                    model3DInstance.version + "%@!" +
                    model3DInstance.ownerId + "%@!" +
                    model3DInstance.taskId + "%@!" +
                    model3DInstance.id
        }
    }
}
